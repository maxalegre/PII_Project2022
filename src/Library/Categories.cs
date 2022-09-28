using System.Collections.Generic;

namespace Library;

public class Categories {
    public List<string> Category = new List<string>();

    protected void Add(string category){
        this.Category.Add(category);
    }
}