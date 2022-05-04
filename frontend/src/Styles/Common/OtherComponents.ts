import { css } from "@emotion/react";
import { gray2 } from "../../Styles/Common/Colors";

export const confirmAlertContainer = css`
  padding: 15px;
  border: solid;
  border-radius: 10px;
  border-width: 2px;
  background-color: white;
`;

export const confirmAlertTitle = css`
  text-decoration: none;
  color: ${gray2};
  padding: 10px 0px;
  font-size: 19px;
`;

export const confirmAlertMessage = css`
  padding: 10px 0px;
  font-size: 17px;
  color: ${gray2};
`;
