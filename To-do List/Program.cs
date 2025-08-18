//
const string filePath = "To-do List.txt";
List<string> todoList = LoadListsFromFile();

Console.WriteLine("Welcome to your to-do list!");


while (true)
{
    Console.WriteLine("Please choose your action:");
    Console.WriteLine(
        "1. Create new list\n" +
        "2. View your list\n" +
        "3. Update your list\n" +
        "4. Delete a list\n" +
        "5. Clear entire list\n" +
        "0. Exit the app");

    int input = 0;
    if (!int.TryParse(Console.ReadLine(), out input))
    {
        Console.WriteLine("Invalid input! Please try again.");
        System.Threading.Thread.Sleep(3000);
        Console.Clear();
        continue;
    }
    //
    switch (input)
    {
        case 1:
            Console.WriteLine("Please type your list to add into the list:");
            todoList.Add(Console.ReadLine());
            SaveListsToFile(todoList);
            DisplayList(todoList);
            break;
        case 2:
            DisplayList(todoList);
            break;
        case 3:
            if (GetTargetListIndex(todoList, out int updateIndex))
            {
                Console.WriteLine("What are your new list content?");
                todoList[updateIndex] = Console.ReadLine();
                SaveListsToFile(todoList);
            }
            DisplayList(todoList);
            Console.ReadKey();
            break;
        case 4:
            Console.WriteLine("Which list do you want to remove:");
            int removeIndex = 0;
            if (int.TryParse(Console.ReadLine(), out removeIndex) && (removeIndex - 1 >= 0 && removeIndex - 1 < todoList.Count()))
            {
                todoList.RemoveAt(removeIndex - 1);
                SaveListsToFile(todoList);
                Console.WriteLine("Your chosen list has been successfully removed.");
            }
            else
            {
                Console.WriteLine("Invalid input! Please choose a valid list number!");
                System.Threading.Thread.Sleep(3000);
            }
            DisplayList(todoList);
            break;
        case 5:
            Console.WriteLine("Do you want to delete all your list? Y/N");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                todoList.Clear();
                SaveListsToFile(todoList);
                Console.WriteLine("Your list has all been deleted!");
            }
            else
            {
                Console.WriteLine("Deletion cancelled!");
            }
            break;
        case 0: return;
        default:
            Console.WriteLine("Invalid input! Please enter a valid command!");
            break;
    }



    


}//while end bracket


static void DisplayList(List<string> todoList)
{
    Console.Clear();
    Console.WriteLine("To-do List");
    if (!todoList.Any())
    {
        Console.WriteLine("Your list is empty! Please try again after you add any list!");
    }
    else
    {
        for (int i = 0; i < todoList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {todoList[i]}");
        }
        Console.WriteLine();
    }
}

static bool GetTargetListIndex(List<string> todoList, out int index)
{
    Console.WriteLine("Which list number do you want to modify:");
    if (int.TryParse(Console.ReadLine(), out index))
    {
        index = index - 1;
        if (index >= 0 && index < todoList.Count)
        {
            return true;
        }

        else
        {
            Console.WriteLine("Invalid index number! Please try again.");
            return false;
        }
    }

    Console.WriteLine("Invalid number! Please input a valid number.");
    return false;
}

static void SaveListsToFile(List<string> todoList)
{
    File.WriteAllLines(filePath, todoList);
}

static List<string> LoadListsFromFile()
{
    if (File.Exists(filePath))
    {
        string[] tempLists = File.ReadAllLines(filePath);
        return new List<string>(tempLists);
    }

    else
    {
        return new List<string>();
    }
}
