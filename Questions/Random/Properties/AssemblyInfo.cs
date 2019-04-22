using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// Enable parallelization of unit testing
[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]