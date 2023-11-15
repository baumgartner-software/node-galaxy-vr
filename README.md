<p align="center">
  <img src="https://raw.githubusercontent.com/baumgartner-software/node-galaxy-vr/main/icon.jpeg" width="256" height="256" alt="Node Galaxy VR Icon">
</p>


# Node Galaxy VR

Node Galaxy VR provides an immersive environment to visualize and interact with graphs, showcasing nodes and edges within a virtual reality space.

<p align="center">
  <img src="https://raw.githubusercontent.com/baumgartner-software/node-galaxy-vr/main/787BC469-4168-4853-AD1E-27FA6FA7F2AD.gif" width="256" height="256" alt="Node Galaxy VR Icon">
</p>


**Disclaimer**: This project is open source; however, all rights are reserved. The current version is in beta. While it's not available in the Meta Quest 3 store, advanced users can manually install the APK with developer mode enabled on their Meta Quest 3 devices.

## Current Features
- **3D Visualization**: Nodes and edges are rendered in a detailed 3D environment suitable for VR.
- **Meta Quest 3 Compatibility**: Fully supported on the Meta Quest 3 platform.
- **Scene Variety**: Choose between various scenes, including Passthrough, White Grid, and Dark Grid.
- **File Interaction**: Capability to load graph data from local JSON storage.
- **Shapes**: Allow different simple shapes

## Transpiler

https://github.com/baumgartner-software/node-galaxy-vr-transpile


## Roadmap

### Pending Implementations:

#### File Management:

- [high] Able to load files from the "Downloads" folder from the Quest3
- [medium] Able to save (a modified) graph back to a file (request user input for name)


#### User Interface:

- [high] Show Controls: Introduction guide showcasing VR controls, designed to be user-dismissible.
  - Can be reopened in the Main-Menu via "show controls"
  - Place a file "controls.png" in the project, which can be replaced later


#### Performance:

- [high] Reduce graph "wobbling" when user moves
  - The Skybox (light grid) does not wobble but the graph
  - Maybe it is the amount of nodes? Maybe add a warning when there are more than X nodes (test X for a stable number)
  - Efforts to achieve a higher and more consistent FPS.
- [medium] Shapes:
  
    - 1. Allowing int the "nodes.json" file that each node can give its shape: [tetrahedron, cube, octahedron, dodecahedron, icosahedron] (https://danielsieger.com/blog/2021/01/03/generating-platonic-solids.html) (https://en.wikipedia.org/wiki/Platonic_solid)
      - Each node can have a field: "shape" (if not, the use default shape: our sphere)
    - 2. Or global option in Main-Menu to change shape of all nodes, if 1. is not possible


#### Main-Menu:

- [high] Better Menu display:
  - Menu is attacked to one (left) controller (similar to OpenBrush https://www.researchgate.net/publication/354823557/figure/fig1/AS:1072625992351745@1632745373599/Rotatable-menu-in-OpenBrush-attached-to-one-controller-showing-the-brush-selection-menu.ppm)
  - Right controller selects
- Main-Menu:
  - Scene:
    - Persisting graph when changing scene
    - Current Scene Selector: Passhough, light Grid, dark Grid
  - Load:
    - Current load Menu
  - Tools:
    - See "Graph Interactions"
  - Options:
    - [medium] Graph-Rotation: 
      - Z-Axis (rotation like a human would spin around clockwise)
      - Button ("slower"): decrease rotation speed, if < 0 then set to 0
      - Textfield: Current speed
      - Button ("faster"): increase roation speed
      - Select yourself a nice step value, which you like
    - [high] Edges:
      - Change the thickness percentage of the edges: "1/X"
        - A button left with an arrow reduces the X (minimum: 1)
        - A button right with an arrow increased the X (maximum: 100)
  - Show controls (see "User Interface")


#### Graph Interactions

- Nodes-Information:
  - [high] Change Node shall be highlighted when Controller is near a Node / hovered over a node
  - [high] "Drag and Reposition" highlightes Node when using "Thumb Button"
    - Node will follow position of Controller until released, then stays there
  - [high] Node Selection (using "Trigger Button")
    - Information shall be displayed at the Menu on the Left Controller (See Main-Menu: Better Menu display)
    - Node-Information Menu can be closed
  - [medium] Node-Information Menu:
    - Next to "additional" also a field: "active" if given in "nodes.json"
      - Field: "active": boolean
        - Node with a true "active" field are highlighted (but different color than the selection/hovering highlight)
    - [medium] "additional": string -> Text field value can be edited
    - Shape: Select/Change the shape of the node (See "Performance")
- [medium] Tools-Menu:
  - "Create Node":
    - Selecting this using "Trigger Button" spawns a Node directly infront of the user. He then can grab the Node using the "Drag and Reposition" feature
  - "Create Edge":
    - Selecting this using "Trigger Button" spawns a small edge/line directly infront of the user. The edge is rendered between the point in the air and the controller. He then needs to select the first node using "Trigger Button". After that the edge is rendered between the node and the controller. He then needs to select the second node.

## Installation & Build

- [Mac] Look at README_IMPORT_MAC.md
- Open Unity Hub & add the project & open the project
- In Unity --> File --> Build Settings --> Android --> Build --> "nodeGalaxy.apk"


## Feedback

Your feedback and contributions are invaluable. We invite collaborators and users to partake in refining and enhancing the Node Galaxy VR experience.
