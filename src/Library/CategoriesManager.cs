using System;
using System.Collections.Generic;

namespace Library;

public class CategoriesManager {
    private List<Category> categories { get; set; }= new List<Category>();

    public CategoriesManager() {

    }
    public void addCategorie(string nameCategorie){
        Category category = new Category(nameCategorie);
        categories.Add(category);
    }

    public void getCategories() {
        foreach (Category item in this.categories)
        {
            System.Console.WriteLine(item.Name);
        }
    }

    /*public void Populate() {
        String[] init = new String[]
        {"Aerospace Engineer",
        "Electrical Engineer",
        "Chemical Engineer",
        "Nuclear Engineer",
        "Bank Manager",
        "Mechanic",
        "Chef"};
        this.categories.AddRange(init);
    }*/
}