import java.io.DataOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.List;

import kong.unirest.HttpResponse;
import kong.unirest.JsonNode;
import kong.unirest.Unirest;
import kong.unirest.json.JSONObject;

public class APICaller {
    public static boolean SendCustomerDataToDatabase(List<Customer> customers, String url) {
        boolean result = true;

        for (Customer customer : customers) {
            String json = "{\"customerRef\":\"" + customer.getCustomerRef() + "\","
                    + "\"customerName\":\"" + customer.getCustomerName() + "\","
                    + "\"addressLine1\":\"" + customer.getAddressLine1() + "\","
                    + "\"addressLine2\":\"" + customer.getAddressLine2() + "\","
                    + "\"town\":\"" + customer.getTown() + "\","
                    + "\"county\":\"" + customer.getCounty() + "\","
                    + "\"country\":\"" + customer.getCountry() + "\","
                    + "\"postcode\":\"" + customer.getPostcode() + "\"}";

            // Create a JSONObject instance
            JSONObject jsonObject = new JSONObject();

            // Set the properties of the JSONObject
            jsonObject.put("customerRef", customer.getCustomerRef());
            jsonObject.put("customerName", customer.getCustomerName());
            jsonObject.put("addressLine1", customer.getAddressLine1());
            jsonObject.put("addressLine2", customer.getAddressLine2());
            jsonObject.put("town", customer.getTown());
            jsonObject.put("county", customer.getCounty());
            jsonObject.put("country", customer.getCountry());
            jsonObject.put("postcode", customer.getPostcode());

            HttpResponse<JsonNode> response = Unirest.post(url)
                    .header("Content-Type", "application/json")
                    .body(jsonObject)
                    .asJson();

            if(response.getStatus() != 200){
                result = false;
                System.out.println(customer.getCustomerRef() + " was not inserted into the database");
            }
        }
        return result;
    }
}
