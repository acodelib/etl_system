package routines;

import java.util.regex.Pattern;
import java.util.regex.Matcher;
import java.util.regex.PatternSyntaxException;

public class JobUtils {

    /**
     * Checks its argument for being null and optionally, if an instance of String is provided, for being empty
     * and aborts execution with non-zero code. 
     */
    public static void abortIfNotGiven(Object parameter, String parameterName) {
        if (parameter == null || parameter instanceof String && ((String) parameter).isEmpty()) {
        	System.out.printf("Required job parameter \"%s\" is not given, aborting job execution.%n", parameterName);
        	System.exit(1);
        }
    }
    
    public static void abortIfDoesntMatch(String parameter, String parameterName, String regex) throws PatternSyntaxException {
    	Pattern pattern = Pattern.compile(regex);
    	Matcher matcher = pattern.matcher(parameter);
    	
    	if (!matcher.matches()) {
    		System.out.printf("Job parameter \"%s\" doesn't match pattern, aborting job execution.%n", parameterName);
    		System.exit(1);
    	}
	}
    
}
