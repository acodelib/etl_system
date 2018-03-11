package routines;

import java.text.DecimalFormat;
import java.text.DecimalFormatSymbols;
import java.text.NumberFormat;
import java.text.ParseException;
import java.util.Locale;

/*
 * user specification: the function's comment should contain keys as follows: 1. write about the function's comment.but
 * it must be before the "{talendTypes}" key.
 * 
 * 2. {talendTypes} 's value must be talend Type, it is required . its value should be one of: String, char | Character,
 * long | Long, int | Integer, boolean | Boolean, byte | Byte, Date, double | Double, float | Float, Object, short |
 * Short
 * 
 * 3. {Category} define a category for the Function. it is required. its value is user-defined .
 * 
 * 4. {param} 's format is: {param} <type>[(<default value or closed list values>)] <name>[ : <comment>]
 * 
 * <type> 's value should be one of: string, int, list, double, object, boolean, long, char, date. <name>'s value is the
 * Function's parameter name. the {param} is optional. so if you the Function without the parameters. the {param} don't
 * added. you can have many parameters for the Function.
 * 
 * 5. {example} gives a example for the Function. it is optional.
 */
public class NumericUtils {

	/**
     * formatUsNumber: return Number value.
     *
     *
     * {talendTypes} String
     *
     * {Category} NumericUtils
     *
     * {param} string("$1,249.99") input: The string need to be parsed.
     *
     * {example} formatUsNumber("$1,249.99") # "1249.99".
     */

    public static Float formatUsNumber(String value) throws ParseException {
        NumberFormat numberFormat = new DecimalFormat("$###,###.00", new DecimalFormatSymbols(Locale.US));
        return numberFormat.parse(value).floatValue();
    }
    
    /**
     * parseFloat: check String for nulls and "-" symbols and return Float value
     *
     *
     * {talendTypes} Float
     *
     * {Category} NumericUtils
     *
     * {param} string("-") input: The string need to be parsed.
     *
     * {example} parseFloat("-") # "0".
     */
    
    public static Float parseFloat(String value) {
    	Float floatValue = null;
    	
    	if (value != null) {
    		floatValue = Float.parseFloat(value.replace("-", "0")); 
    	} else {
    		floatValue = Float.parseFloat("0");
    	}
    	
    	return floatValue;
    }
    
    /**
     * parseInt: check String for nulls and "-" symbols and return Integer value
     *
     *
     * {talendTypes} Integer
     *
     * {Category} NumericUtils
     *
     * {param} string("-") input: The string need to be parsed.
     *
     * {example} parseInt("-") # "0".
     */
    
    public static Integer parseInt(String value) {
    	Integer intValue = null;
    	
    	if (value != null) {
    		intValue = Integer.parseInt(value.replace("-", "0")); 
    	} else {
    		intValue = Integer.parseInt("0");
    	}
    	
    	return intValue;
    }
}
