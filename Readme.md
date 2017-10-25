# Document Reconstruction
## Overview
 Reconstructing shredded documents plays a very important role in reducting the material evidence, recovering histroical documents, obtaining the military intelligence, etc. Traditionlly, reconstructing shredded documents is a manual work. Though it has high accuracy rate, it has low efficency, espacially having a large amount of shredded strips. It is difficult for human to complete in a very short time. With the computer technique developing, we are trying to develop a software to do it automatically. This project is trying to find out a algorithm to reconstruct different type of documents. The project is from 2013 math modeling competetion.

## Programming Language

This project is developed by C# using Visual Studio 2010

## Inputs
1. Two pages of documents, one is in Chinese, the other one is in English, shredded vertically by paper shredder.
2. Two pages of documents, one is in Chinese, the other one is in English, shredded both vertically and horizontally by paper shredder.

## Process
### 1. Binaryzation
Setting a threshold and making the all pixels in pictures in only black and white.
### 2. Finding all neighbors
A strip is a two deminsional array. The column 0 is the left edge of the strip, and n-1 column is the right edge of the strip. When strip A's [i,0] and another strip B's [i,n-1] are both black. We add 1 to a variable m. Using C(n-1) represents the number of black pixel on the right edge.Compute the ratio of m to B's C(n-1). The pair has the maximum ratio may be neighbor.
### 3. Generating the document
Put the strips in order and output them into a new picture.
