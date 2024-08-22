import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class CSVParser {
    public static List<Customer> ParseCSV(String filePath) throws IOException
    {
        List<Customer> customers = new ArrayList<>();
        try (BufferedReader br = new BufferedReader(new FileReader(filePath))) {
            String line;
            while ((line = br.readLine()) != null) {
                String[] columns = line.split(",");
                if (columns.length == 8) {
                    String customerRef = columns[0];
                    String customerName = columns[1];
                    String addressLine1 = columns[2];
                    String addressLine2 = columns[3];
                    String town = columns[4];
                    String county = columns[5];
                    String country = columns[6];
                    String postcode = columns[7];
                    customers.add(new Customer(
                            customerRef, customerName, addressLine1, addressLine2, town, county, country, postcode
                    ));
                    System.out.println("Parsed: " + customerRef);
                } else {
                    System.out.println("Invalid line: " + line);
                }
            }
        }

        return customers;
    }

}
