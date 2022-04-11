import { css } from "@emotion/react";
import { gray2, gray6 } from "../Common/Colors";
import { mainFontFamily, mainFontSize } from "../Common/Fonts";

export const appointmentContainer = css`
  background-color: white;
  padding: 15px 20px 20px 20px;
  border-radius: 4px;
  border: 1px solid ${gray6};
  box-shadow: 0 3px 5px 0 rgba(0, 0, 0, 0.16);
`;

export const appointmentTitle = css`
  font-size: 19px;
  font-weight: bold;
  margin: 10px 0px 5px;
`;

export const appointmentAdditionalInf = css`
  font-family: ${mainFontFamily};
  font-size: ${mainFontSize};
  padding: 5px 10px;
  background-color: transparent;
  color: ${gray2};
  text-decoration: none;
`;
