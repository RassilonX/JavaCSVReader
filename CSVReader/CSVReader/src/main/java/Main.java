import java.io.IOException;
import java.util.List;

//TIP To <b>Run</b> code, press <shortcut actionId="Run"/> or
// click the <icon src="AllIcons.Actions.Execute"/> icon in the gutter.
public class Main {
    public static void main(String[] args) throws IOException {
        //TIP Press <shortcut actionId="ShowIntentionActions"/> with your caret at the highlighted text
        // to see how IntelliJ IDEA suggests fixing it.
        String filePath = "C:\\Users\\RassilonX\\Desktop\\JavaCSVReader\\CustomerTestData.csv";
        System.out.println("Parsing CSV file located at: " + filePath);
        List<Customer> customers = CSVParser.ParseCSV(filePath);

        System.out.println("Sending data to the database");
        String databaseUrl = "https://localhost:1701/Customer";
        boolean result = APICaller.SendCustomerDataToDatabase(customers, databaseUrl);

        if(result)
        {
            System.out.println("All data parsed Successfully");
        }
        else
        {
            System.out.println("There were errors parsing the data");
        }
    }
}