using System;
using System.Collections.Generic;

namespace Library;

public sealed class CategoriesManager {
    private List<Category> categories { get; set; }= new List<Category>();

    private static CategoriesManager instance;

    public static CategoriesManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CategoriesManager();
            }

            return instance;
        }
    }
    public CategoriesManager() {

    }
    public void addCategory(string nameCategorie){
        Category category = new Category(nameCategorie.ToUpper());
        if(!categories.Contains(category)){
            categories.Add(category);
        }
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