//
using To_do_List;
//Define a file path and name to store and load the todo list
const string filePath = "To-do List.txt";
//A list that stores ListContent by calling a method to load the contents which stores the description and status of the lists, or a new list if theres no file exist
List<ListContent> todoList = LoadListsFromFile();

Console.WriteLine("Welcome to your to-do list!");

//Main logic where the actions are take in the while loop
while (true)
{
    //Ask user to chose action
    Console.WriteLine("Please choose your action:");
    Console.WriteLine(
        "1. Create new list\n" +
        "2. View your list\n" +
        "3. Update your list\n" +
        "4. Delete a list\n" +
        "5. Clear entire list\n" +
        "0. Exit the app");

    //If the input is not a int number, ask to re enter
    int input = 0;
    if (!int.TryParse(Console.ReadLine(), out input))
    {
        Console.WriteLine("Invalid input! Please try again.");
        System.Threading.Thread.Sleep(3000);
        Console.Clear();
        continue;
    }

    //Switch case that perform the logic based on user input
    switch (input)
    {
        //Adding a new content to the todo list
        case 1:
            Console.WriteLine("Please type your content to add into the list:");
            string description = Console.ReadLine();
            ListContent newList = new ListContent(description);//using constructor to define and assign new instance and add to the list
            todoList.Add(newList);
            SaveListsToFile(todoList);//Perform a save to file when the list is changed
            DisplayList(todoList);//Display the list everytime a change is made
            break;
        case 2:
            DisplayList(todoList);
            break;
        case 3:
            DisplayList(todoList);
            //Call method to get the index user wish to change
            if (GetTargetListIndex(todoList, out int updateIndex))
            {
                //Store that in a separate variable
                ListContent listToUpdate = todoList[updateIndex];
                //Ask user their action for this list
                Console.WriteLine($"You are updating {listToUpdate.Description}");
                Console.WriteLine("Choose your action:\n" +
                    "1. Mark as complete/incomplete\n" +
                    "2. Update list content");

                //Validation check for the user's input
                bool validActionInput = int.TryParse(Console.ReadLine(), out int actionInput);
                if (validActionInput && actionInput == 1)
                {
                    listToUpdate.IsCompleted = !listToUpdate.IsCompleted;//Flip over the value to revert it
                }
                else if (validActionInput && actionInput ==2)
                {
                    Console.WriteLine("What are your new list content?");
                    listToUpdate.Description = Console.ReadLine();//Update the list with user's input
                }
                else
                {
                    Console.WriteLine("Invalid input, Please try again!");
                    continue;
                }
                //Perform a save to the file
                SaveListsToFile(todoList);
            }
            //Display the new list to the user
            DisplayList(todoList);
            Console.ReadKey();//Wait until user press key
            break;
        case 4:
            DisplayList(todoList);
            //Console.WriteLine("Which list do you want to remove:");
            //int removeIndex = 0;
            if (GetTargetListIndex(todoList, out int removeIndex))
            {
                todoList.RemoveAt(removeIndex);
                SaveListsToFile(todoList);
                Console.WriteLine("Your chosen list has been successfully removed.");
            }
            
            DisplayList(todoList);
            Console.ReadKey();
            break;
        case 5:
            //A double check ask the user wishes to delete all the lists
            Console.WriteLine("Do you want to delete all your list? Y/N");
            if (Console.ReadLine().ToUpper() == "Y")//Only proceeding when the input is y/Y
            {
                //Clear list and store to the file
                todoList.Clear();
                SaveListsToFile(todoList);
                Console.WriteLine("Your list has all been deleted!");
            }
            else
            {
                Console.WriteLine("Deletion cancelled!");
            }
            break;
        case 0: return;//Quit the switch case and end the while loop
        default:
            Console.WriteLine("Invalid input! Please enter a valid command!");//Any other invalid input will be ask to start again
            break;
    }



    


}//while end bracket


//Method that display the todo list
static void DisplayList(List<ListContent> todoList)
{
    Console.Clear();
    Console.WriteLine("To-do List");
    //Check the list has any elements
    if (!todoList.Any())
    {
        Console.WriteLine("Your list is empty! Please try again after you add any list!");
    }
    else
    {
        //Iterate over the list
        for (int i = 0; i < todoList.Count; i++)
        {
            //Assign value based on the property's value and display
            string status = todoList[i].IsCompleted ? "[X]" : "[ ]";
            Console.WriteLine($"{i + 1}. {status} {todoList[i]}");
        }
        Console.WriteLine();
    }
}

//Method that ask user to input the index of element to update
static bool GetTargetListIndex(List<ListContent> todoList, out int index)
{
    //Get the index and convert to int, if fails return false and error message
    Console.WriteLine("Which list number do you want to modify:");
    if (int.TryParse(Console.ReadLine(), out index))
    {
        //Convert to 0 based index and valid that the index is in a valid range
        index = index - 1;
        if (index >= 0 && index < todoList.Count)
        {
            return true;
        }

        else
        {
            Console.WriteLine("Invalid index number! Please try again.");//Return false because not in a valid range
            System.Threading.Thread.Sleep(3000);
            return false;
        }
    }

    Console.WriteLine("Invalid number! Please input a valid number.");
    return false;
}

//A simple method that convert and check the user input is a valid int or not
static bool IndexValidationCheck (out int index)
{
    if (int.TryParse(Console.ReadLine(), out index))
    {
        return true;
    }
    return false;
}

//Method that save all the lists in a given file path
static void SaveListsToFile(List<ListContent> todoList)
{
    //List that stores string
    List<string> lists = new List<string>();
    //Iterate all the elements in the todolist
    foreach (ListContent list in todoList)
    {
        //Add to the string list with their properties value
        lists.Add($"{list.IsCompleted}|{list.Description}");
    }
    //Using WriteAllLines method to store in the path given
    File.WriteAllLines(filePath, lists);
}

//Method that load todo list from the fail path given
static List<ListContent> LoadListsFromFile()
{
    //A new list that stores the ListContent 
    List<ListContent> contents = new List<ListContent>();
    //Check the file exists
    if (File.Exists(filePath))
    {
        //Load lists from the file path and store in an array of string
        string[] lists = File.ReadAllLines(filePath);
        foreach (string line in lists)
        {
            //Find and separate each element in the lists with | and store in another array of string
            string[] parts = line.Split('|');
            //Validty check that it contains the value we want and avoid empty access
            if (parts.Length == 2)
            {
                //The second element(index 1) will be Description property based on the format we stored and use for creating ListContent instance
                string description = parts[1];
                //Parse the string false or true to bool true and false
                bool isCompleted = bool.Parse(parts[0]);
                //Define and assign instance by these values and add to the list
                ListContent loadedTask = new ListContent(description);
                loadedTask.IsCompleted = isCompleted;
                contents.Add(loadedTask);
            }
        }
    }
    //After complete loading and adding to the list, return the list
    return contents;
}
