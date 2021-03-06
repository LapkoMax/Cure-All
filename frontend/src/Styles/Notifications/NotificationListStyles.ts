import { css } from "@emotion/react";
import { accent2, gray2, gray5 } from "../Common/Colors";

export const notificationList = css`
  list-style: none;
  margin: 10px 0 0 0;
  padding: 0px 20px;
  background-color: #fff;
  border-bottom-left-radius: 4px;
  border-bottom-right-radius: 4px;
  border-top: 3px solid ${accent2};
  box-shadow: 0 3px 5px 0 rgba(0, 0, 0, 0.16);
`;

export const notificationComponent = css`
  text-decoration: none;
  padding-top: 5px;
  color: ${gray2};
  border-top: 1px solid ${gray5};
  :first-of-type {
    border-top: none;
  }
`;
