using System;

namespace Library;

public class Admin : Categories
{
    public void addCategory(string category){
        if (!string.IsNullOrEmpty(category))
        {
            this.Add(category);
        } 
        else
        {
            Console.WriteLine("Entrada inválida");
        }
    }

    public void removeService(string service){
        /* 
        Como identificar el servicio a eliminar?
        Para eliminar un servicio necesitamos saber cual es exactamente,
        ya sea con un ID, o igualando la descripción de tal servicio (No parece ser muy sencillo o eficiente)
        */
    }
}