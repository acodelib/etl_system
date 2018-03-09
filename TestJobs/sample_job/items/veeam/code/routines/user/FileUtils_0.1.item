package routines;

import java.io.File;
import java.io.IOException;
import java.nio.file.DirectoryNotEmptyException;
import java.nio.file.NoSuchFileException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.lang.IllegalArgumentException;

public class FileUtils {

    public static boolean directoryIsEmpty(String path) {
    	File file = new File(path);
    	
    	if (!file.exists())      throw new IllegalArgumentException("file/directory doesn't exist");
    	if (!file.isDirectory()) throw new IllegalArgumentException("not a directory");
    	
    	return file.list().length == 0;
    }
    
    // Verbose replacement for tFileDelete -- if file can't be deleted it will let you know 
    public static void delete(String path) {
    	try {
    	    Files.delete(Paths.get(path));
    	} catch (NoSuchFileException e) {
    	    System.err.format("%s: no such" + " file or directory%n", path);
    	} catch (DirectoryNotEmptyException e) {
    	    System.err.format("%s not empty%n", path);
    	} catch (IOException e) {
    	    System.err.println(e);
    	}
    }
    
}
