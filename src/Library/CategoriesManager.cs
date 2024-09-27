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
    private CategoriesManager() {

    }
    public void addCategory(string nameCategorie){
        Category category = new Category(nameCategorie.ToUpper());
        if(!categories.Contains(category)){
            categories.Add(category);
        }
    }

    public List<Category> getCategories() {
        return this.categories;
    }
}
