using System;

namespace OOP05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Part 01 : Theoretical Questions

                #region Q1 : What is an interface in C#? Why do we use it? Benefits?
                /*
                 * 1. Definition: 
                 * An interface is a "Contract." It tells a class what methods and properties it must have, but it does not write the code for them.
                 * * 2. Why use it: 
                 * It allows us to write flexible code that doesn't depend on one specific class. We can swap different classes easily as long as they follow the same interface.
                 * * 3. Three Benefits:
                 * - Loose Coupling: Changes in one part of the code don't break everything else
                 * - Multiple Implementation: A class can follow many interfaces at the same time
                 * - Standardizing Code: It ensures that different classes provide the same functionality like "Print" or "Clone"
                 */
                #endregion

                #region Q2 : Interface Naming Conflicts 
                /*
                 * a) The Problem: 
                 * Currently, there is ambiguity. The class has one Greet() method that tries to satisfy both interfaces at once
                 * * b) The Fix: 
                 * Use "Explicit Interface Implementation"
                 * Example: 
                 * void IEnglishSpeaker.Greet() { Console.WriteLine("Hello"); }
                 * void IArabicSpeaker.Greet() { Console.WriteLine("Ahlan"); }
                 * * c) Calling the Method: 
                 * No, you cannot call it directly from the object (e.g., translator.Greet() will not work)
                 * To call it, you must cast the object to the interface type first:
                 * ((IEnglishSpeaker)translator).Greet();
                 */
                #endregion

                #region Q3 : Shallow Copy vs. Deep Copy
                /*
                 * 1. Shallow Copy: 
                 * Copies the object’s values. If the object contains another object reference type, it only copies the memory address the "pointer"
                 * * 2. Deep Copy: 
                 * Creates a completely new and independent copy of the object and all other objects inside it
                 * * 3. The Risk: 
                 * In a shallow copy, if you change a reference-type field in the copy, it will also change in the original object because they share the same memory
                 */
                #endregion

                #region Q4 : Code Snippet Output Analysis
                /*
                 * Output:
                 * Dev - Testing
                 * QA - Testing
                 * * Why:
                 * - The Title ("Dev","QA") changed only for e2 because it is a value-like type string.
                 * - The Dept.Name "Testing" changed for BOTH e1 and e2. 
                 * - This happened because ShallowCopy used MemberwiseClone(), 
                 *   which copies the reference to the Department object instead of creating a new one
                 */
                #endregion

            #endregion

           
        }
    }
}