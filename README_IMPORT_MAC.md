AudioPluginOculusSpatializer problem

!!!
!!! it is in: Assets/Oculus/Spatializer/Plugins/AudioPluginOculusSpatializer.bundle
!!!


Here's the solution:
1. Open the MacOS terminal.
2. Enter the command: xattr -d com.apple.quarantine /.../Assets/Plugins/Whatever.bundle
3. Do this for every .bundle you have, and they once again work.


See more information on the command here:
https://www.unix.com/man-page/osx/1/xattr/

I found the solution here at first: https://developer.maxst.com/BoardQuestions/Details/813, but this clears all attributes. It's probably better to only target the attribute that causes the problem, which I found with the base xattr command. 
