namespace Library;

public class Offer
{
    public string Description;
    public double Remuneration;
    public string Category;
    public bool state;
    public Employee employee;
    public Employer employer;

    public Offer (Employee employee,string Description , double Remuneration, string category)
    {
        this.Description = Description;
        this.Remuneration = Remuneration;
        this.Category = category;
        this.state=true;
        this.employee=employee;
    }
    // Hice una sobrecarga 
    public Offer (Employer employer,string Description , double Remuneration, string category)
    {
        this.Description = Description;
        this.Remuneration = Remuneration;
        this.Category = category;
        this.state=true;
        this.employer=employer;
    }

}

