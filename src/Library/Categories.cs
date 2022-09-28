using System;
using System.Collections.Generic;

namespace Library;

public class Categories {
    private List<string> Category { get; set; }= new List<string>();

    public Categories() {
        Populate();
    }
    protected void Add(Categories cat, string category){
        cat.Category.Add(category);
    }

    public void getCategories() {
        foreach (string item in this.Category)
        {
            System.Console.WriteLine(item);
        }
    }

    /*
    public void removeCategory() {
        System.Console.WriteLine("Que categoria quiere eliminar? : ");
        string a = Console.ReadLine();
        int indice = this.Category.IndexOf(a);
        this.Category.RemoveAt(indice);
    }
    */

    public void Populate() {
        String[] init = new String[]
        {"Aerospace Engineer",
        "Electrical Engineer",
        "Chemical Engineer",
        "Nuclear Engineer",
        "Bank Manager",
        "Mechanic",
        "Chef"};
        this.Category.AddRange(init);
    }
}