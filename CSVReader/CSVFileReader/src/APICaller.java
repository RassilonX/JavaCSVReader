import java.io.DataOutputStream;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.List;



public class APICaller {
    public static boolean SendCustomerDataToDatabase(List<Customer> customers, String url) throws IOException {
        boolean result = true;

        for (Customer customer : customers) {
            URL apiUrl = new URL(url);
            HttpURLConnection con = (HttpURLConnection) apiUrl.openConnection();
            con.setRequestMethod("POST");
            con.setRequestProperty("Content-Type", "application/json; utf-8");
            con.setRequestProperty("Accept", "application/json");
            con.setDoOutput(true);

            String json = "{\"CustomerRef\":\"" + customer.getCustomerRef() + "\","
                    + "\"CustomerName\":\"" + customer.getCustomerName() + "\","
                    + "\"AddressLine1\":\"" + customer.getAddressLine1() + "\","
                    + "\"AddressLine2\":\"" + customer.getAddressLine2() + "\","
                    + "\"Town\":\"" + customer.getTown() + "\","
                    + "\"County\":\"" + customer.getCounty() + "\","
                    + "\"Country\":\"" + customer.getCountry() + "\","
                    + "\"Postcode\":\"" + customer.getPostcode() + "\"}";

            try (DataOutputStream os = new DataOutputStream(con.getOutputStream())) {
                os.writeBytes(json);
                os.flush();
            }

            int status = con.getResponseCode();

            if(status != 200){
                result = false;
                System.out.println(customer.getCustomerRef() + " was not inserted into the database");
            }
        }
        return result;
    }
}
