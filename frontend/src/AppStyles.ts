import { css } from "@emotion/react";
import { gray2 } from "./Styles/Common/Colors";
import { mainFontFamily, mainFontSize } from "./Styles/Common/Fonts";
import doctorSmiling from "./Content/doctorSmiling.jpg";

export const appContainer = css`
  font-family: ${mainFontFamily};
  font-size: ${mainFontSize};
  color: ${gray2};
  min-height: 100vh;
  min-width: 100vh;
`;

export const appContainerWithBackGround = css`
  font-family: ${mainFontFamily};
  font-size: ${mainFontSize};
  color: ${gray2};
  min-height: 100vh;
  min-width: 100vh;
  background-image: url(${doctorSmiling});
  background-size: cover;
`;
