using System;

namespace BusinessEntities
{
  public class Project : IdObject
  {
    public string Name { get; private set; }

    public void SetName(string name)
    {
      if (string.IsNullOrEmpty(name))
      {
        throw new ArgumentNullException("Name was not provided.");
      }
      Name = name;
    }
  }
}
