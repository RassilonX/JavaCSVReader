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
                    String customerRef = columns[0].replaceAll("\"", "");
                    String customerName = columns[1].replaceAll("\"", "");
                    String addressLine1 = columns[2].replaceAll("\"", "");
                    String addressLine2 = columns[3].replaceAll("\"", "");
                    String town = columns[4].replaceAll("\"", "");
                    String county = columns[5].replaceAll("\"", "");
                    String country = columns[6].replaceAll("\"", "");
                    String postcode = columns[7].replaceAll("\"", "");
                    customers.add(new Customer(
                            customerRef, customerName, addressLine1, addressLine2, town, county, country, postcode
                    ));
                    System.out.println("Parsed: " + customerRef);
                } else {
                    System.out.println("Invalid line: " + line);
                }
            }
        }

        customers.remove(0);
        return customers;
    }
}
