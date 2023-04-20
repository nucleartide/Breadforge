# Workflow

**Water shader**

- Included d scripts
- Water volume game object
    - Position in the bottom left of the area where you want water
    - Add a mesh renderer
    - Add a water volume (transforms) component
    - Apply a material using the stylized water shader to the renderer
    - Adjust the water volume’s tile size to define the resolution of the generated mesh
    - A smaller tile size will result in a higher resolution
    - Create empty child game objects within the water volume to define the volume covered
    - Position and scale the transforms accordingly, and their volumes will be snapped to the tile grid and a mesh generated to encapsulate them
    - While the wave animation is handled in the shader, and affected by the water volume’s transform and mesh, helper methods are available to determine the height of the water at a given world-space coordinate (use this for player animation)
    - This can be useful for placing objects floating on the water’s surface, or implementing gameplay logic.
    - As an example, create a sphere and place it within the water volume with a Water Volume Floater component attached
        - There’s a rim light around the sphere object in the water
    - You will also need to add a water volume helper component to your scene, and provide it a reference to the water volume you wish to interact with
    - The object will now adhere to the water volume’s surface.
    - Try opening the component’s script to see how to utilize this functionality in your own scripts.
- **Water styles**
    - **3 sample materials are included, and can be tried out by dragging them onto your water volume. Original, realistic, and surreal**
    - **You can, of course, also make your own custom variations. Create a new material and select the WaterVolume-URP shader. While the shader is technically opaque, you must set its render queue to Transparent to allow for the refraction effect.**
    - **The color and opacity of the water in shallower and deeper areas can be modified separately, allowing for variety and some interesting effects**
    - **How glossy and how metallic the surface of the water is can be modified. While higher glossiness values and lower metallic values tend to work best, experimenting with these can uncover cool effects.**
    - **The UV scale settings allow for tweaking the resolution and tiling of the normal map**
    - **The detail scale setting lets you adjust the resolution and tiling of the 2nd normal map layer relative to the base UV scale**
    - **The detail strength settings allow for adjusting the balance between the 1st and 2nd normal map layer, usually emphasizing the larger waves over the smaller details**
    - **The scroll speed setting controls how quickly the 2 normal map layers move over the surface**
    - **The wave frequency refers to the number of vertex animated waves to appear over an area of world space, with a higher value resulting in more peaks and troughs**
    - **The wave scale setting manages the vertical size that the peaks of the waves will reach**
    - **The wave speed setting controls how quickly the vertex animated waves move over the surface**
    - **The overall impact that the combined normal map layers have on the lighting of the water’s surface can be controlled via the bump strength**
    - **The refract strength setting defines the level of distortion that will be applied to anything rendered through the water, scaled based on relative depth**
    - **The foam width setting controls how far in from the edges the foam effect will spread while the foam noise value controls how distorted the foam is by the surface**
    - **The depth scale value defines the distance at which an object beneath the water is considered to be at the maximum depth for the purposes of opacity, color, and refraction**
    - **The depth power value is the exponent which controls the interpolation between min and max depth with a value of 1 being linear, a value of 0.5 being a gradual curve, and a value of 2 being sharper**
    - **As well as manually via vertex painting, foam can be automatically placed through depth comparison. This is especially useful for adding wash to dynamic objects.**
- Big circular enemies and eyes that look at you.
