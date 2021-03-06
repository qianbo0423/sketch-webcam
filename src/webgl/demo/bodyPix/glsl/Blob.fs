precision highp float;

varying vec3 vPosition;

void main() {
  // Flat Shading
  vec3 light = normalize(vec3(-1.0, 1.0, 1.0));
  vec3 normal = normalize(cross(dFdx(vPosition), dFdy(vPosition)));
  float diff = dot(normal, light);

  gl_FragColor = vec4(vec3(0.9, 0.2, 0.4) + diff * 0.5, 1.0);
}
