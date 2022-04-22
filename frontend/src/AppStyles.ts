import { css } from "@emotion/react";
import { gray2 } from "./Styles/Common/Colors";
import { mainFontFamily, mainFontSize } from "./Styles/Common/Fonts";
import doctorSmiling from "./Content/doctorSmiling.jpg";

export const appContainer = css`
  font-family: ${mainFontFamily};
  font-size: ${mainFontSize};
  color: ${gray2};
  min-height: 100vh;
  box-sizing: border-box;
  min-width: 100vw;
  width: 100vw;
  overflow-x: hidden;
  overflow-y: inherit;
  display: flex;
  justify-content: space-between;
  background-image: url(${doctorSmiling});
  background-size: cover;
  background-position: center;
  background-position-y: 30px;
  background-repeat: no-repeat;
  background-attachment: fixed;
`;
