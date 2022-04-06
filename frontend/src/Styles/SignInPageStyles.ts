import { css } from "@emotion/react";
import { gray2, gray5 } from "./Common/Colors";
import { mainFontFamily, mainFontSize } from "./Common/Fonts";

export const registerLink = css`
  font-family: ${mainFontFamily};
  font-size: ${mainFontSize};
  background-color: transparent;
  color: ${gray2};
  text-decoration: none;
  cursor: pointer;
  :focus {
    outline-color: ${gray5};
  }
  span {
    margin-left: 5px;
  }
`;
