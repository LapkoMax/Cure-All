import { css } from "@emotion/react";
import { green1, green2 } from "../Common/Colors";

export const availableTimeNotSelected = css`
  background-color: ${green2};
  color: black;
  border-radius: 5px;
  border-style: solid;
  border-color: ${green1};
  cursor: pointer;
`;

export const availableTimeSelected = css`
  background-color: ${green1};
  color: black;
  border-radius: 5px;
  border-radius: 5px;
  border-style: solid;
  border-color: ${green1};
  cursor: pointer;
`;
