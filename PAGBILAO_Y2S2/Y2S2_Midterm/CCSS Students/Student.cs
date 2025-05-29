using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{
    public string name;
    public int year;
    public Gender gender;
    public Category category;

    public void GoToSchool()
    {
        Debug.Log($"{name} goes to school");
    }
}

public enum Gender {Male, Female, Other}
public enum Category {Regular, Irregular}