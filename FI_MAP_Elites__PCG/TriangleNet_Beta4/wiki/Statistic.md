# Understanding the Statistic tab

The _Statistic_ tab shows mesh statistics and quality information. It is divided into 4 categories, which will be described in the following.

## 1 Elements statistic
Displays the number of elements (vertices, segments and triangles). The first column shows the current number of elements, the second column the number of elements in the previous mesh (or input geometry). Using the two columns, you can easily track any changes made to the mesh during refinement.

## 2 Geometry statistic
Displays geometry information (triangle area, edge length and angle). Using the two columns - minimum and maximum, you can get an overall view of mesh quality and decide if the mesh needs further refinement.

## 3 Quality measures
Displays mesh quality information (minimum angle and aspect ratio). Both measures are scaled to lie between 0 and 1, while 0 defines the worst and 1 the best possible value. The first column shows the minimum value, the second column the average over all triangles in the mesh.

The _minimum angle_ of a triangle may lie between 0 and 60 degrees, so a measure of 0.75 for example means the triangle has a minimum angle of 45 degrees. The optimal value of 1 means the triangle is equilateral.

The _aspect ratio_ of a triangle is defined as the ratio of the circumradius to twice its inradius. Just like the minimum angle, it measures the uniformity of the triangle. A value of 1 means the triangle is equilateral.

Often the minimum quality value plays an important role in finite element analysis. But also keep in mind that the minimum may be low because of small input angles.

## 4 Angle histogram
Visualizes the distribution of minimum and maximum angles of all triangles in the mesh. The minimum angles (0 to 60 degrees) are displayed on the left (green color). Maximum angles (60 to 180 degrees) are to the right (blue color).

