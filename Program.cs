using NLog;

class Program
{
    // initialize a logger instance
    private static Logger logger = LogManager.GetCurrentClassLogger();

    // file paths for different types of data
    private const string moviesFilePath = "movies.csv";
    private const string ratingsFilePath = "ratings.csv";
    private const string linksFilePath = "links.csv";
    private const string tagsFilePath = "tags.csv";

    static void Main(string[] args)
    {
        Console.WriteLine("Your Movies Library");

        // creating main loop using while loop to interact with user
        while (true)
        {
            // creating selection for user to choose
            Console.WriteLine("\nYour selection:");
            Console.WriteLine("1. Add new movie");
            Console.WriteLine("2. View all movies");
            Console.WriteLine("3. View all ratings");
            Console.WriteLine("4. View all tags");
            Console.WriteLine("5. View all links");
            Console.WriteLine("Enter any key to exit");

            // reading user choice 
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // using method to add a new movie
                    AddNewMovie();
                    break;
                case "2":
                    ViewAllMovies();
                    break;
                case "3":
                    ViewAllRatings();
                    break;
                case "4":
                    ViewAllTags();
                    break;
                case "5":
                    ViewAllLinks();
                    break;
                default:
                    // exit the program if any other key is pressed
                    Environment.Exit(0);
                    break;
            }

        }
    }

    // method to view all movies
    static void ViewAllMovies()
    {
        // calling a method to view entities from file
        ViewAllEntitiesFromFile(moviesFilePath, "MovieID", "Title", "Genres");
    }

    // method to view all links
    static void ViewAllLinks()
    {
        ViewAllEntitiesFromFile(linksFilePath, "MovieID", "IMDB ID", "TMDB ID");
    }

    // method to view all tags
    static void ViewAllTags()
    {
        ViewAllEntitiesFromFile(tagsFilePath, "UserID", "MovieID", "Tag", "TimeStamp");
    }

    // method to view all ratings
    static void ViewAllRatings()
    {
        ViewAllEntitiesFromFile(ratingsFilePath, "UserID", "MovieID", "Rating", "TimeStamp");
    }

    // method to view all entities from a file
    static void ViewAllEntitiesFromFile(string filePath, params string[] headers)
    {
        try
        {
            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"No data found in {Path.GetFileName(filePath)}.");
                return;
            }

            // print header for the type of data being viewed
            Console.WriteLine($"All {Path.GetFileNameWithoutExtension(filePath)}:");

            // Read each line from the file and print its fields
            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] fields = line.Split(',');
                for (int i = 0; i < headers.Length; i++)
                {
                    // print each field with its header
                    Console.Write($"{headers[i]}: {fields[i].Trim()}, ");
                }
                // move to the next line after printing all fields
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            // Print error message if an exception occurs
            Console.WriteLine($"An error occurred: {ex.Message}");
            // log the error using NLog
            logger.Error(ex, $"An error occurred while processing file: {filePath}");
        }
    }

    // Method to add a new movie
    static void AddNewMovie()
    {
        try
        {
            Console.WriteLine("Enter movie details in the format: MovieID,Title,Genres");
            string newMovie = Console.ReadLine();

            // Check if the input is not empty
            if (!string.IsNullOrWhiteSpace(newMovie))
            {
                // Append the new movie details to the file
                using (StreamWriter writer = File.AppendText(moviesFilePath))
                {
                    writer.WriteLine(newMovie);
                }
                Console.WriteLine("Movie added successfully.");
            }
            else
            {
                // print error message for invalid input
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
        catch (Exception ex)
        {
            // print error message if an exception occurs
            Console.WriteLine($"An error occurred: {ex.Message}");
            // log the error using NLog
            logger.Error(ex, "An error occurred while adding a new movie.");
        }
    }
}
