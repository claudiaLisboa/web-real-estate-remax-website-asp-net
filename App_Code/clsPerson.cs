using System;

public abstract class Person
{
    private int _personId;
    private string _firstName;
    private string _lastName;
    private DateTime _birthDate;
    private string _email;
    private Gender _gender;
    private string _phone;
    private City _city;

    // Default constructor
    public Person()
    {
        _personId = default(int);
        _firstName = string.Empty;
        _lastName = string.Empty;
        _birthDate = default(DateTime);
        _email = string.Empty;
        _gender = default(Gender);
        _phone = string.Empty;
        _city = default(City);
    }

    public DateTime BirthDate
    {
        get { return _birthDate; }
        set { _birthDate = value; }
    }

    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    public string FullName
    {
        get { return _firstName + ' ' + _lastName; }
    }

    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    public int PersonId
    {
        get { return _personId; }
        set { _personId = value; }
    }

    public string Phone
    {
        get { return _phone; }
        set { _phone = value; }
    }

    public City City
    {
        get { return _city; }
        set { _city = value; }
    }

    public Gender Gender
    {
        get { return _gender; }
        set { _gender = value; }
    }
}
