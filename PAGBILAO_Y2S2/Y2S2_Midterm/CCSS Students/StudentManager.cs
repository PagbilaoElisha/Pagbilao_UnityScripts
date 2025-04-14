using System; // Avoid missing type or namespace name for GetInheritanceDepth(Type type)
using System.Collections;
using System.Collections.Generic;
using System.Linq; // Required for .Where() and .OrderBy(); used in PerformAllStudentFunctions()
using UnityEngine;
using System.Reflection; // Required in MethodInfo; used in PerformAllStudentFunctions()

public class StudentManager : MonoBehaviour
{
    public List<Student> students = new List<Student>();
    public List<ITStudent> iTStudents = new List<ITStudent>();
    public List<CSStudent> cSStudents = new List<CSStudent>();
    public List<DSStudent> dSStudents = new List<DSStudent>();
    public List<EMCStudent> eMCStudents = new List<EMCStudent>();
    public List<DAStudent> dAStudents = new List<DAStudent>();
    public List<GDStudent> gDStudents = new List<GDStudent>();

    public GameObject studentParent;

    void Start()
    {
        students.AddRange(studentParent.GetComponentsInChildren<Student>()); // Add all students in one GetComponentsInChildren<>(); arranged according to Scene Hierarchy
        foreach (Student s in students) // Assigning students in student list to their respective sub-classes
        {
            switch (s)
            {
                case ITStudent i:
                    iTStudents.Add(i);break;
                case CSStudent c:
                    cSStudents.Add(c); break;
                case DSStudent d:
                    dSStudents.Add(d); break;
                case DAStudent a:
                    dAStudents.Add(a);
                    eMCStudents.Add(a); break;
                case GDStudent g:
                    gDStudents.Add(g); 
                    eMCStudents.Add(g); break;
            }
        }
        PerformAllStudentFunctions();
    }

    void PerformAllStudentFunctions()
    {
        foreach (Student student in students)
        {
            // Efficient way to collect all user-defined functions in 7 scripts
            MethodInfo[] methods = student.GetType().
                GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => !method.ContainsGenericParameters && !method.IsSpecialName) // Avoid InvalidOperatorException and NotSupportedException errors
                .OrderBy(method => GetInheritanceDepth(method.DeclaringType)) // Sort classes (Student -> (IT, CS, DS and EMC) -> (DA and GD))
                .ToArray();
            foreach (MethodInfo method in methods)
            {
                if (method.GetParameters().Length == 0)
                {
                    // Performs all functions, starting with the base class function (GoToSchool())
                    method.Invoke(student, null);
                }
            }
        }
    }

    // Determiner of inheritance depth; used in PerformAllStudentFunctions()
    int GetInheritanceDepth(Type type)
    {
        int depth = 0;
        while (type.BaseType != null)
        {
            depth++;
            type = type.BaseType;
        }
        return depth;
    }
}