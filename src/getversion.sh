# Look for line starting with [assembly: System.Reflection.AssemblyVersion( to get the assembly version
gawk -F\" '$1 == "[assembly: System.Reflection.AssemblyVersionAttribute(" { print $2 }' AssemblyVersionInfo.cs