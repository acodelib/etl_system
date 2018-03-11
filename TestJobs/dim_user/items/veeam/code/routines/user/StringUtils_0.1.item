package routines;

import java.util.Arrays;
import java.util.List;

public class StringUtils {
    /**
     * helloExample: return index value from string separated by '_'
     *
     *
     * {talendTypes} String
     *
     * {Category} StringUtils
     *
     * {param} string("some_text_to_split") input: The string need to be parsed.
     *
     * {example} StringPart("some_text_to_split", 2) returns 'to'.
     */

    public static String StringPart(String value, int index) {
    	int counter = 0;
        StringBuilder buffer = new StringBuilder();

        try {
            for (int i = 0; counter <= index; i++) {
                char currentChar = value.charAt(i);
                if (currentChar == '_') counter++;
                if (counter == index) break;
                buffer.append(currentChar);
            }
        } catch (StringIndexOutOfBoundsException e) {
            return null;
        }

        String newValue = buffer.toString();
        return newValue.substring(newValue.lastIndexOf('_') + 1, newValue.length());
    }
    
    public static boolean isNullOrEmpty(String string) {
    	return string == null || string.isEmpty();
    }   

    public static String replaceNullOrEmpty(String string, String replacement) {
    	return isNullOrEmpty(string) ? replacement : string;
    }   

    public static String replaceNull(String string, String replacement) {
    	return string == null ? replacement : string;
    }

    public static String replaceNull(String string) {
    	return replaceNull(string, "");
    }
    
    public static List<String> splitToList(String str, char delimiter) {
    	return Arrays.asList(str
    			.trim()
    			.split("\\s*" + delimiter + "\\s*"));
    }
}
