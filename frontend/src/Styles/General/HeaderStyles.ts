import { css } from "@emotion/react";
import { gray1, gray2, gray5 } from "../Common/Colors";
import { mainFontFamily, mainFontSize } from "../Common/Fonts";

export const headerContainer = css`
  position: fixed;
  box-sizing: border-box;
  top: 0;
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 10px 20px;
  background-color: #fff;
  border-bottom: 1px solid ${gray2};
  box-shadow: 0 3px 7px 0 rgba(110, 112, 114, 0.21);
`;

export const titleAnchor = css`
  font-size: 24px;
  font-weight: bold;
  color: ${gray1};
  text-decoration: none;
`;

export const searchInput = css`
  box-sizing: border-box;
  font-family: ${mainFontFamily};
  font-size: ${mainFontSize};
  padding: 8px 10px;
  border: 1px solid ${gray5};
  border-radius: 3px;
  color: ${gray2};
  background-color: white;
  width: 200px;
  height: 30px;
  :focus {
    outline-color: ${gray5};
  }
`;

export const signInAnchor = css`
  font-family: "Segoe UI", "Helvetica Neue", sansserif;
  font-size: 16px;
  padding: 5px 10px;
  background-color: transparent;
  color: #5c5a5a;
  text-decoration: none;
  cursor: pointer;
  :focus {
    outline-color: #e3e2e2;
  }
  span {
    margin-left: 5px;
  }
`;

export const helloUserLabel = css`
  font-family: "Segoe UI", "Helvetica Neue", sansserif;
  font-size: 16px;
  padding: 5px 10px;
  background-color: transparent;
  color: #5c5a5a;
  text-decoration: none;
`;
