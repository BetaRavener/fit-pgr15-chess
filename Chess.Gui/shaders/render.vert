#version 400

in vec4 vert;

uniform mat4 mvpMatrix;

void main()
{
    gl_Position = mvpMatrix * vert;
}