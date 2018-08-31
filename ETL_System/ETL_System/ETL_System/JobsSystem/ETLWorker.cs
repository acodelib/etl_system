using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ETL_System {
    public class ETLWorker {
        //================================ FIELDS ===============================================
        public string name;

        private JobsQueue jobs_queue;
        private Job target_job;
        private CoreDB data_layer;
        private int job_instance_id;
        private bool exec_result;
        private JobInstance job_instance;

        private NotificationsManager notifier;
        private int work_frequency;
        string output; // should convert to string builder in future build
        string error;
        int exit_code;

        private System.Object _locker = new System.Object();  // local critical zone lockingc

        //================================ CONSTRUCTOR ==========================================
        public ETLWorker() { }

        public ETLWorker(JobsQueue the_queue,NotificationsManager the_mailer,CoreDB data_layer,int frequency) {
            this.jobs_queue = the_queue;
            this.notifier = the_mailer;
            this.work_frequency = frequency;
            this.data_layer = data_layer;         
        }


        //================================ METHODS ==============================================
        public void startExecutionRoutine() {
            Thread t = new Thread(fetchAndExecuteCycle);
            t.Start();
        }
     

        private void fetchAndExecuteCycle() {
            Thread.Sleep(357);
            Console.WriteLine($"Worker {this.name} started.");
            this.job_instance = new JobInstance();
            string exec_path;
            this.output = null;
            this.error = null;
            this.exit_code = 0;
            while (1 == 1) {
                if (SystemSharedData.workers_start_flag) {
                    //fetch job
                    this.target_job = this.jobs_queue.provideJobToRun();
                    if (this.target_job != null) {
                        try {
                            //record start
                            recordStart(this.target_job);
                            
                            //set execution path
                            exec_path = SystemSharedData.jobs_folder + @"\" + this.target_job.executable_name;

                            //execute job file and read outputs async
                            this.asyncExecuteJobFile(exec_path);                                                        
                        }
                        catch (Exception e) {
                            this.error = e.Message;
                        }
                        try {
                            //Thread.Sleep(5000); //dev simulate some work;
                            Console.WriteLine("OUTPUT: " + this.output);
                            Console.WriteLine("ERROR: " + this.error);
                            Console.WriteLine(this.exit_code.ToString());

                            //check the outcome of the job and record Job Instance
                            if (this.exit_code != 0)
                                this.recordEnd(false);
                            else
                                this.recordEnd(true);

                        }catch(Exception e) {

                            LogManager.writeErrorToLog("Fatal Error! Worker failure: " + e.Message, AppDomain.CurrentDomain.BaseDirectory + "ETLSystemLog.etl");
                            notifier.sendErrorEmailMessage(target_job.notifiactions_list, target_job.name,$"ETL Worker failed to complete its task !!!! Error:{e.Message}");
                            lock (_locker) {
                                this.target_job.is_executing = false;
                                this.jobs_queue.dequeueJob(this.target_job.job_id);
                                this.target_job.executing_timestamp = null;
                                this.target_job.executor = null;
                                this.target_job = null;
                            }
                            Console.WriteLine("Fatal Error! System shuts down with the message: " + e.Message);
                            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory + "ETLSystemLog.etl");

                        }
                    }
                    Console.WriteLine($"{this.name} did some work, going for more...");
                    Thread.Sleep(this.work_frequency * 1000);  // just determines the rest period for each worker.
                }
            }
            
        }               

        private void asyncExecuteJobFile(string exec_path) {
            var process_context = new ProcessStartInfo("cmd.exe", "/c " + exec_path);
            process_context.CreateNoWindow = true;
            process_context.UseShellExecute = false;
            process_context.RedirectStandardError = true;
            process_context.RedirectStandardOutput = true;
            var exec = Process.Start(process_context);
            //receive execution output
            exec.OutputDataReceived += new DataReceivedEventHandler(this.asyncCatchOutputEvent);
            exec.BeginOutputReadLine();
            //catch execution error
            exec.ErrorDataReceived += new DataReceivedEventHandler(this.asyncCatchErrorEvent);
            exec.BeginErrorReadLine();

            exec.WaitForExit();
            exit_code = exec.ExitCode;
            exec.Close();
        }

        private void asyncCatchOutputEvent(object sender, DataReceivedEventArgs e) {
            this.output = this.output + e.Data + "\n";
        }

        private void asyncCatchErrorEvent(object sender, DataReceivedEventArgs e) {
            this.error = this.error + e.Data + "\n";
        }

        private void parseOutput() {
            string[] key_val = this.output.Split('@');
            
            for (int i = 0;i< key_val.Length; i++) {
                switch (key_val[i]) {
                    case "{checkpoint}":
                        if (this.target_job.checkpoint_type == 1) {
                            this.job_instance.old_data_checkpoint = target_job.data_chceckpoint;
                            this.job_instance.new_data_checkpoint = long.Parse(key_val[i + 1].Replace("{", "").Replace("}", "").Replace("\n", ""));
                        }
                        if (this.target_job.checkpoint_type == 2) {
                            this.job_instance.old_time_checkpoint = target_job.time_checkpoint;
                            this.job_instance.new_time_checkpoint = DateTime.Parse(key_val[i + 1].Replace("{", "").Replace("}", "").Replace("\n", ""));
                        }
                        break;
                    case "{rows_inserted}":
                        this.job_instance.rows_inserted =Int32.Parse(key_val[i + 1].Replace("{", "").Replace("}", "").Replace("\n", ""));
                        break;
                    case "{rows_updated}":
                        this.job_instance.rows_updated = Int32.Parse(key_val[i + 1].Replace("{", "").Replace("}", "").Replace("\n", ""));
                        break;
                    case "{rows_deleted}":
                        this.job_instance.rows_deleted = Int32.Parse(key_val[i + 1].Replace("{", "").Replace("}", "").Replace("\n", ""));
                        break;
                }                    
            }
        }

        private void generateJobInstanceDetail() {

        }

        private void recordStart(Job j) {
            lock (_locker) {
                j.is_executing = true;
                j.executing_timestamp = DateTime.Now;
                j.executor = this.name;
            }
        }

        private void recordEnd(bool outcome_success) {
            this.parseOutput();
            target_job.last_instance_timestamp = target_job.executing_timestamp;


            //1.DATA management:
            if (outcome_success) {  // record routine for successfull cases                
                lock (_locker) {
                    if (target_job.checkpoint_type == 1)
                        target_job.data_chceckpoint = this.job_instance.new_data_checkpoint;
                    if (target_job.checkpoint_type == 2)
                        target_job.time_checkpoint = this.job_instance.new_time_checkpoint;                   
                    if (target_job.current_failed_count > 0) {
                        notifier.sendRecoveryEmailMessage(target_job.notifiactions_list, target_job.name);
                        target_job.current_failed_count = 0;
                    }

                    target_job.is_failed = false;
                }
            }
            else {
                lock (_locker) {
                    target_job.current_failed_count++;
                    if (target_job.max_try_count <= target_job.current_failed_count) {
                        target_job.is_failed = true;
                        target_job.is_paused = true;                        
                    }
                    notifier.sendErrorEmailMessage(target_job.notifiactions_list, target_job.name, this.error);
                }
            }



            this.job_instance.result = outcome_success ? "success" : "fail";
            this.job_instance.worker = this.name;
            this.job_instance.output = this.output.Replace(@"'", "");
            this.output = null;
            this.job_instance.error = this.error.Replace(@"'", "");
            this.error = null;


            this.data_layer.recordJobInstance(target_job, outcome_success, this.job_instance);

            //finalize and dequeue
            lock (_locker) {
                this.target_job.is_executing = false;
                this.jobs_queue.dequeueJob(this.target_job.job_id);
                this.target_job.executing_timestamp = null;
                this.target_job.executor = null;                
                this.target_job = null;                
            }
            
            
        }
        private void asyncNotification() {

        }
    }

    public class JobInstance {
        public string result;
        public string worker;
        public DateTime start_timestamp;
        public DateTime end_timestamp;
        public long? old_data_checkpoint;
        public long? new_data_checkpoint;
        public DateTime? old_time_checkpoint;
        public DateTime? new_time_checkpoint;
        public int rows_inserted;
        public int rows_updated;
        public int rows_deleted;
        public string output;
        public string error;
        public int exit_code;
                 
    }
}
