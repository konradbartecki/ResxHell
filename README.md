# ResxHell
Middle-build .resx to .resw conversion and migration tool. Allows for migration of .resx contained in PCL to WinRT projects


##Why ResxHell?

In case you are wondering how to make shared localization library with Xamarin that works on iOS, Android and Windows Phone 8.1 - you could make an shared PCL and place .resx resources there, but it won't really work in WP8.1. By using this tool you can automatically convert .resx to native WP8.1 format .resw and move them to WP project every build, so you don't have to copy it manually to every project everytime PCL with .resx was updated.

###Features
- Convert .resx to .resw (if .resx are used only to store string values)
- Sync .resx from PCL to native .resw in WinRT project on build

##How to use

1.  Check your project build order and make sure that PCL that contains .resx builds before WinRT project ![](http://i.imgur.com/CaI1R9Z.png) ![](http://i.imgur.com/LEpkUs9.png)
2. Create default localization folder structure in your WinRT project for example: ![](http://i.imgur.com/I9FBd3n.png)
  - Strings
      - ar
      - en
3. Make a "ResxHell" folder in your solution's root directory and place a ResxHell.exe there 
  ![](http://i.imgur.com/5p8M9r9.png) ![](http://i.imgur.com/iDBYytQ.png).
4. Go to your PCL project > Properties > Build Events > and in Post-Build event enter 

  ![](http://i.imgur.com/ExeMdB8.png)
  ```
  "$(SolutionDir)\ResxHell\ResxHell.exe" -import "$(ProjectDir)\"
  ```
5. Go to your WinRT project > Properties > Build Events > and in Pre-Build event enter 
  
  ![](http://i.imgur.com/idhNYK1.png)
  ```  
  "$(SolutionDir)\ResxHell\ResxHell.exe" -export "$(ProjectDir)\"
  ```
6. Build your WinRT project - it can fail when building for the first time
7. On solution explorer click "Show all files" button and select all new files > Include in project You will have to do this every time a new .resx is added in a Portable Class Library ![](http://i.imgur.com/y3h9aZI.png)
8. Rebuild again and now you can reach your imported .resw from C# code
