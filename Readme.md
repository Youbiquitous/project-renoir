**Project Renoir** is a web application built on the latest **.NET Core** stack to demonstrate a *clean architecture*. It is centered around 
three fundamental business entities: product, release note, and roadmap. According to their role and assigned permissions, 
logged users can:

- Create and edit products 
- Assign contributors to a product
- Create and edit release notes and roadmap
- Navigate the list of available documents (release notes and roadmaps)
- Export and share documents

In general, a release note document is a concise summary of changes, improvements, and bug fixes introduced in a new software release. 
It serves as a communication tool for informing users and, more importantly, stakeholders about the modifications made to 
a given software product. Release notes highlight key features, enhancements, and resolved issues, providing an overview 
of what users can expect from the new release. It may also include instructions, known issues, and compatibility information 
to help users working with the product more effectively. Release notes, though, may also serve another purpose--documenting the 
work done. By navigating the list of release notes documents, one can track back the history of development and count the number 
of fixes, hot fixes, new features and maintenance that has been done in a range of time. 

From a purely tech standpoint, Renoir uses Blazor for the UI and C#.
