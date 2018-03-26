package routines;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.URL;
import java.net.URLConnection;
import java.net.HttpURLConnection;
import java.util.Map;
import java.util.HashMap;
import java.util.List;
import java.util.ArrayList;
import java.util.zip.GZIPInputStream;
import java.util.regex.Pattern;
import java.util.regex.Matcher;


public class RobustHttpRequest {
	
	private static String buildParameterString(Map<String, String> parameters) {		
		if (parameters != null && !parameters.isEmpty()) {
			List<String> list = new ArrayList<String>();
			
			for (Map.Entry<String, String> entry : parameters.entrySet()) {
				list.add(entry.getKey() + "=" + entry.getValue());
			}
			
			return String.join("&", list);
		} else {
			return "";
		}
	}
	
	private static void setRequestProperties(URLConnection connection, Map<String, String> properties) {
		if (connection != null && properties != null && !properties.isEmpty()) {
			for (Map.Entry<String, String> entry : properties.entrySet()) {
				connection.setRequestProperty(entry.getKey(), entry.getValue());
	        }
		}
	}
	
    public static Map<String, String> send(String method, String baseURL,
                                           Map<String, String> parameters, Map<String, String> headers) throws Exception {
    	String parameterString = RobustHttpRequest.buildParameterString(parameters);
    	
    	return RobustHttpRequest.send(method, baseURL, parameterString, headers);
    }
    
    public static Map<String, String> send(String method, String baseURL, String parameters, Map<String, String> headers) throws Exception {
    	String url = baseURL + (parameters == null || parameters.isEmpty() ? "" : "?" + parameters);
		
    	URL obj = new URL(url);
    	
    	HttpURLConnection con = (HttpURLConnection) obj.openConnection();
        
        con.setRequestMethod(method);
        RobustHttpRequest.setRequestProperties(con, headers);
        
        InputStream rawResponse;
        String exceptionMessage = "";
        try {
            rawResponse = con.getInputStream();
        } catch (IOException e) {
        	exceptionMessage = e.getMessage();
            rawResponse = con.getErrorStream();
        }
        
        String contentEncoding = con.getContentEncoding();
        rawResponse = contentEncoding != null && contentEncoding.contains("gzip") ? new GZIPInputStream(rawResponse) : rawResponse;
        
        Matcher charsetMatcher = Pattern.compile(".*charset=([^;]+).*").matcher(con.getContentType());
        charsetMatcher.matches();
        String charset = charsetMatcher.group(1);
        
        StringBuffer stringResponse = new StringBuffer();        
        try (BufferedReader reader = new BufferedReader(new InputStreamReader(rawResponse, charset))) {
            String inputLine;
            while ((inputLine = reader.readLine()) != null) {
            	stringResponse.append(inputLine);
            }
        }

        Map<String, String> requestResult = new HashMap<String, String>();
        requestResult.put("exceptionMessage", exceptionMessage);
        requestResult.put("response", stringResponse.toString());
        
        return requestResult;
    }
}
