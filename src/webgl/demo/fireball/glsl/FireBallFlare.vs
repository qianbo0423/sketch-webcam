attribute vec3 position;
attribute vec3 normal;
attribute vec2 uv;

uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;
uniform vec3 cameraPosition;
uniform float time;
uniform sampler2D texture;

varying vec2 vUv;

void main() {
  // Coordinate transformation
  float texR = 1.0 - texture2D(texture, uv - vec2(time * 0.1, 0.0)).r;
  float texG = 1.0 - texture2D(texture, uv + vec2(time * 0.2, 0.0)).g;
  float strength = sin(radians((texR * 0.7 + texG * 0.3) * 360.0)) * 0.5 + 0.5;
  vec3 updatePosition = position + normalize(position) * strength * 0.7;

  vec4 mPosition = modelMatrix * vec4(updatePosition, 1.0);

  vUv = uv;

  gl_Position = projectionMatrix * viewMatrix * mPosition;
}
