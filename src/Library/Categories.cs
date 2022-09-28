using System.Collections.Generic;

namespace Library;

public class Categories {
    private List<string> Category { get; set; }= new List<string>();

    protected void Add(Categories cat, string category){
        cat.Category.Add(category);
    }

    public void getCategories() {
        foreach (string item in this.Category)
        {
            System.Console.WriteLine(item);
        }
    }
}