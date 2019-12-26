uniform mat4 rotationX;
uniform mat4 rotationY;
uniform mat4 rotationZ;
uniform mat4 scale;
in vec4 coord;

void main() {
	vec4 posx = rotationX * coord;
	vec4 posy = rotationY * posx;
	vec4 posz = rotationZ * posy;
	gl_Position = scale * posz;
}
