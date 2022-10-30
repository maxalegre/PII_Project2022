namespace Library;

public class Offer
{
    public string Description;
    public double Remuneration;
    public string Category;
    public bool state;
    public Employee employee;

    public Offer (Employee employee,string Description , double Remuneration, string category)
    {
        this.Description = Description;
        this.Remuneration = Remuneration;
        this.Category = category;
        this.state=true;
        this.employee=employee;
    }

}

