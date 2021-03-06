import { Mesh, PlaneBufferGeometry, RawShaderMaterial } from 'three';
import MathEx from 'js-util/MathEx';
import { easeInOutCirc, easeOutCirc } from 'easing-js';

// import store from '@/store';

import vs from './glsl/FireBallAura.vs';
import fs from './glsl/FireBallAura.fs';

const DURATION_SHOW = 2;
const DURATION_HIDE = 1;

export default class FireBallAura extends Mesh {
  constructor() {
    const geometry = new PlaneBufferGeometry(6, 6, 1, 1);

    const material = new RawShaderMaterial({
      uniforms: {
        time: {
          value: 0
        },
        alphaShow: {
          value: 0
        },
        alphaHide: {
          value: 0
        }
      },
      vertexShader: vs,
      fragmentShader: fs,
      transparent: true,
      depthWrite: false
    });

    super(geometry, material);
  }
  update(t, ts, th) {
    const { time, alphaShow, alphaHide } = this.material.uniforms;

    time.value += t;
    alphaShow.value = easeInOutCirc(MathEx.clamp(ts / DURATION_SHOW, 0, 1));
    alphaHide.value = easeOutCirc(MathEx.clamp(th / DURATION_HIDE, 0, 1));

    const scale = 0.4 + alphaShow.value * 0.6 + alphaHide.value * 0.4;
    this.scale.set(scale, scale, scale);
  }
}
