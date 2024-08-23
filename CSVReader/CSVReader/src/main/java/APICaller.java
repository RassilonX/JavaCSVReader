import java.util.List;

import kong.unirest.HttpResponse;
import kong.unirest.JsonNode;
import kong.unirest.Unirest;
import kong.unirest.json.JSONObject;

public class APICaller {
    public static boolean SendCustomerDataToDatabase(List<Customer> customers, String url) {
        boolean result = true;

        for (Customer customer : customers) {
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

            System.out.println("Sending customer to database: " + customer.getCustomerRef());

            HttpResponse<JsonNode> response = Unirest.post(url)
                    .header("Content-Type", "application/json")
                    .body(jsonObject)
                    .asJson();

            if(response.getStatus() != 200){
                result = false;
                System.out.println(customer.getCustomerRef() + " was not inserted into the database");
            }
            else {
                System.out.println(customer.getCustomerRef() + " inserted into the database");
            }
        }
        return result;
    }
}
