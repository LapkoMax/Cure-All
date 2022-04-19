import { css } from "@emotion/react";
import { gray2, gray3 } from "../../Styles/Common/Colors";

export const notificationContainer = css`
  padding: 10px;
  text-decoration: none;
  color: ${gray2};
  font-size: 17px;
`;

export const notificationTitle = css`
  text-decoration: none;
  color: ${gray2};
  padding: 10px 0px;
  font-size: 19px;
`;

export const notificationAdditionalInf = css`
  font-size: 16px;
  font-style: italic;
  padding-top: 15px;
  color: ${gray3};
`;

export const unreadNotificationSpan = css`
  background-color: red;
  color: white;
  border-radius: 5px;
  padding: 3px;
`;
