import { css } from "@emotion/react";
import styled from "@emotion/styled";
import { gray2, gray5, gray6 } from "./Colors";
import { mainFontFamily, mainFontSize } from "./Fonts";

export const Fieldset = styled.fieldset`
  margin: 10px auto 0 auto;
  padding: 30px;
  width: 350px;
  background-color: ${gray6};
  border-radius: 4px;
  border: 1px solid ${gray5};
  box-shadow: 0 3px 5px 0 rgba(0, 0, 0, 0.16);
`;

export const RegistrationFieldset = styled.fieldset`
  margin: 10px auto 0 auto;
  padding: 30px;
  width: 700px;
  background-color: ${gray6};
  border-radius: 4px;
  border: 1px solid ${gray5};
  box-shadow: 0 3px 5px 0 rgba(0, 0, 0, 0.16);
`;

export const EditFieldset = styled.fieldset`
  margin: 10px auto 0 auto;
  padding: 30px;
  width: 700px;
  background-color: ${gray6};
  border-radius: 4px;
  border: 1px solid ${gray5};
  box-shadow: 0 3px 5px 0 rgba(0, 0, 0, 0.16);
`;

export const FieldContainer = styled.div`
  margin-bottom: 10px;
`;

export const FieldLabel = styled.label`
  font-weight: bold;
`;

const baseFieldCSS = css`
  box-sizing: border-box;
  font-family: ${mainFontFamily};
  font-size: ${mainFontSize};
  margin-bottom: 5px;
  padding: 8px 10px;
  border: 1px solid ${gray5};
  border-radius: 3px;
  color: ${gray2};
  background-color: white;
  width: 100%;
  :focus {
    outline-color: ${gray5};
  }
  :disabled {
    background-color: ${gray6};
  }
`;

export const FieldInput = styled.input`
  ${baseFieldCSS}
`;

export const FieldSelect = styled.select`
  ${baseFieldCSS}
`;

export const FieldOption = styled.option`
  ${baseFieldCSS}
`;

export const FieldTextArea = styled.textarea`
  ${baseFieldCSS}
  height: 100px;
`;

export const FieldError = styled.div`
  font-size: 14px;
  color: red;
  height: 50px;
`;

export const SubmissionSuccess = styled.div`
  margin-top: 10px;
  color: green;
`;

export const SubmissionFailure = styled.div`
  margin-top: 10px;
  color: red;
`;
