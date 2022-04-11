import { css } from "@emotion/react";
import { gray2, gray3 } from "../../Styles/Common/Colors";

export const appointmentContainer = css`
  padding: 10px 0px;
`;

export const appointmentTitle = css`
  text-decoration: none;
  color: ${gray2};
  padding: 10px 0px;
  font-size: 19px;
`;

export const appointmentAdditionalInf = css`
  font-size: 16px;
  font-style: italic;
  padding-top: 15px;
  color: ${gray3};
`;
