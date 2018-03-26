package routines;

import com.opencsv.CSVWriter;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.nio.charset.Charset;
import java.util.List;
import java.util.function.Function;

import org.apache.commons.io.input.ReversedLinesFileReader;

public class CSVUtils {

    /**
     * Writes data given as a list of rows (each row is a list of strings)
     * to specified path using a specified separator.
     *
     * {Category} CSVUtils
     *
     * {param} List<List<String>> data : the data
     * 
     * {param} String file : full path to the output file
     * 
     * {param} char separator : columns separator character
     *
     */
    public static void writeFile(List<List<String>> data, String filepath, char separator) throws IOException {    	
    	try (CSVWriter writer = new CSVWriter(new OutputStreamWriter(new FileOutputStream(filepath), Charset.forName("UTF-8")), separator)) {
    		for (List<String> row : data) {
    			if (row == null)
    				continue;
    			writer.writeNext(row.toArray(new String[0]));
    		}
    	}
    }
    
    /**
     * Returns line number starting from 1 of the file's header, inclusively.
     * If no suitable line is found -1 is returned.
     *
     * {talendTypes} int | Integer
     *
     * {Category} CSVUtils
     *
     * {param} String filepath : path to the file
     * 
     * {param} String encoding : encoding of the file
     * 
     * {param} Function<String, Boolean> criteria : a criteria by which the line will be identified as a header
     *
     * {example} locateFileHeader("example.csv", "UTF-8", line -> line.startsWith("Header"))
     *
     */    
    public static int locateFileHeader(String filepath, String encoding, Function<String, Boolean> criteria) throws IOException {
        int rowcount = 1;        
        Charset charset = Charset.forName(encoding);
        try(BufferedReader reader = new BufferedReader(new InputStreamReader(new FileInputStream(filepath), charset))) {
            String line;
            while ((line = reader.readLine()) != null) {
                if (criteria.apply(line))
                    return rowcount;
                rowcount++;
            }
        }

        return -1;
    }
    
    /**
     * Returns line number starting from 1 of the file's footer, inclusively.
     * If no suitable line is found -1 is returned.
     *
     * {talendTypes} int | Integer
     *
     * {Category} CSVUtils
     *
     * {param} String filepath : path to the file
     * 
     * {param} String encoding : encoding of the file
     * 
     * {param} Function<String, Boolean> criteria : a criteria by which the line will be identified as a header
     *
     * {example} locateFileFooter("example.csv", "UTF-8", line -> line.startsWith("Footer"))
     *
     */
    public static int locateFileFooter(String filepath, String encoding, Function<String, Boolean> criteria) throws IOException {
    	int rowcount = 1;

    	File file = new File(filepath);
    	int blockSize = 4096;  // default in Apache Commons IO since 2.5
    	Charset charset = Charset.forName(encoding);

    	try(ReversedLinesFileReader reversed = new ReversedLinesFileReader(file, blockSize, charset)) {
    	    String line;
    	    while ((line = reversed.readLine()) != null) {
    	        if (criteria.apply(line))
    	            return rowcount;
	            rowcount++;
    	    }
    	}
    	
    	return -1;
    }
    
}
