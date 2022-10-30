using System;
using System.Collections.Generic;

using Library;

public  class CategoriesService
{
private  List<Category> Categories { get; set; }= new List<Category>();

 static CategoriesService(){}
public void addCategory(string nameCategory){
        if (!string.IsNullOrEmpty(nameCategory))
        {
            Category category= new Category(nameCategory);
            Categories.Add(category);
        } 
        else
        {
            Console.WriteLine("Entrada inválida");
        }
        }
}