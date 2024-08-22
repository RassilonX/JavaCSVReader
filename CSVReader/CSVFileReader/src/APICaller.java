import java.io.IOException;
import java.util.List;

import org.apache.http.HttpEntity;
import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;
import org.apache.http.util.EntityUtils;

public class APICaller {
    public static boolean SendCustomerDataToDatabase(List<Customer> customers, String url) throws IOException
    {
        CloseableHttpClient httpClient = HttpClients.createDefault();

        for (Customer customer : customers) {
            HttpPost post = new HttpPost("https://example.com/api/endpoint");
            String json = "{\"customerRef\":\"" + customer.getCustomerRef() + "\","
                    + "\"customerName\":\"" + customer.getCustomerName() + "\","
                    + "\"addressLine1\":\"" + customer.getAddressLine1() + "\","
                    + "\"addressLine2\":\"" + customer.getAddressLine2() + "\","
                    + "\"town\":\"" + customer.getTown() + "\","
                    + "\"county\":\"" + customer.getCounty() + "\","
                    + "\"country\":\"" + customer.getCountry() + "\","
                    + "\"postcode\":\"" + customer.getPostcode() + "\"}";
            StringEntity entity = new StringEntity(json);
            post.setEntity(entity);
            post.setHeader("Accept", "application/json");
            post.setHeader("Content-type", "application/json");

            CloseableHttpResponse response = httpClient.execute(post);
            try {
                System.out.println(response.getStatusLine().getStatusCode());
                HttpEntity responseEntity = response.getEntity();
                System.out.println(EntityUtils.toString(responseEntity));
            } finally {
                response.close();
            }
        }
        return true;
    }

}
