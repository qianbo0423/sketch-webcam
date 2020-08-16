precision highp float;

uniform float time;
uniform sampler2D texture;

varying vec3 vPosition;
varying vec2 vUv;

#pragma glslify: ease = require(glsl-easings/circular-in-out);
#pragma glslify: convertHsvToRgb = require(glsl-util/convertHsvToRgb);

void main() {
  vec3 light = normalize(vec3(0.0, 1.0, 1.0));
  vec3 normal = normalize(cross(dFdx(vPosition), dFdy(vPosition)));
  float diff = dot(normal, light);

  // Define Colors
  float texR1 = texture2D(texture, vUv - vec2(time * 0.05, 0.0)).r;
  float texR2 = 1.0 -texture2D(texture, vUv + vec2(time * 0.14, 0.0)).g;
  float strength = sin(radians((texR1 + texR2) * 360.0)) * 0.5 + 0.5;
  vec3 hsv = vec3(
    strength * 0.14 + 0.03,
    0.95 - strength * 0.8,
    strength * 0.4 + 0.8
    );
  vec3 rgb = convertHsvToRgb(hsv);

  gl_FragColor = vec4(rgb, 1.0);
}
