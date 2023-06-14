using UnityEngine;
using System.Collections;
using System; //This allows the IComparable Interface

//This is the class you will be storing
//in the different collections. In order to use
//a collection's Sort() method, this class needs to
//implement the IComparable interface.
public class Database : IComparable<Database>
{
    public string name;
    public int power;

    public Database(string newName, int newPower)
    {
        name = newName;
        power = newPower;
    }

    //This method is required by the IComparable
    //interface. 
    public int CompareTo(Database other)
    {
        if (other == null)
        {
            return 1;
        }

        //Return the difference in power.
        return power - other.power;
    }
}